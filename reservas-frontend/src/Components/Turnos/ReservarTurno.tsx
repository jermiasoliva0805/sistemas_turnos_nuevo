import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../Context/AuthContext';
import { turnosApi } from '../../API/turnos.api';
import { empleadosApi } from '../../API/empleados.api';
import { serviciosApi } from '../../API/servicios.api';
import { Empleado, Servicio, ReservarTurnoRequest } from '../../Types';
import './Turnos.css';

export const ReservarTurno: React.FC = () => {
    const { user } = useAuth();
    const navigate = useNavigate();
    const [empleados, setEmpleados] = useState<Empleado[]>([]);
    const [servicios, setServicios] = useState<Servicio[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const [formData, setFormData] = useState({
        empleadoId: '',
        servicioId: '',
        fecha: '',
        horaInicio: '',
        horaFin: '',
    });

    // Cargar empleados y servicios al montar
    useEffect(() => {
        cargarDatos();
    }, []);

    const cargarDatos = async () => {
        try {
            setLoading(true);
            const [empleadosData, serviciosData] = await Promise.all([
                empleadosApi.obtenerTodos(),
                serviciosApi.obtenerTodos(),
            ]);
            setEmpleados(empleadosData);
            setServicios(serviciosData);
        } catch (err) {
            setError('Error al cargar empleados y servicios');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        if (!user) {
            setError('Debes estar autenticado para reservar un turno');
            return;
        }

        if (!formData.empleadoId || !formData.servicioId || !formData.fecha || !formData.horaInicio || !formData.horaFin) {
            setError('Todos los campos son requeridos');
            return;
        }

        try {
            setLoading(true);
            const request: ReservarTurnoRequest = {
                UsuarioId: user.id_Usuarios,
                EmpleadoId: parseInt(formData.empleadoId),
                ServicioId: parseInt(formData.servicioId),
                Fecha: formData.fecha,
                HoraInicio: formData.horaInicio + ':00', // Agregar segundos
                HoraFin: formData.horaFin + ':00', // Agregar segundos
            };

            await turnosApi.reservar(request);
            setSuccess('Turno reservado exitosamente!');
            setFormData({ empleadoId: '', servicioId: '', fecha: '', horaInicio: '', horaFin: '' });
            
            setTimeout(() => {
                navigate('/mis-turnos');
            }, 2000);
        } catch (err: any) {
            setError(err.response?.data?.message || 'Error al reservar turno');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    if (loading && empleados.length === 0) {
        return <div className="container"><p>Cargando...</p></div>;
    }

    return (
        <div className="container">
            <div className="card">
                <h2>Reservar Turno</h2>

                {error && <div className="alert alert-error">{error}</div>}
                {success && <div className="alert alert-success">{success}</div>}

                <form onSubmit={handleSubmit} className="form">
                    <div className="form-group">
                        <label>Empleado</label>
                        <select
                            name="empleadoId"
                            value={formData.empleadoId}
                            onChange={handleChange}
                            required
                            className="form-input"
                        >
                            <option value="">Selecciona un empleado</option>
                            {empleados.map((emp) => (
                                <option key={emp.Id_Empleados} value={emp.Id_Empleados}>
                                    {emp.NombreCompleto}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Servicio</label>
                        <select
                            name="servicioId"
                            value={formData.servicioId}
                            onChange={handleChange}
                            required
                            className="form-input"
                        >
                            <option value="">Selecciona un servicio</option>
                            {servicios.map((srv) => (
                                <option key={srv.Id_Servicios} value={srv.Id_Servicios}>
                                    {srv.Nombre} - ${srv.Precio}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Fecha</label>
                        <input
                            type="date"
                            name="fecha"
                            value={formData.fecha}
                            onChange={handleChange}
                            required
                            className="form-input"
                        />
                    </div>

                    <div className="form-group">
                        <label>Hora Inicio</label>
                        <input
                            type="time"
                            name="horaInicio"
                            value={formData.horaInicio}
                            onChange={handleChange}
                            required
                            className="form-input"
                        />
                    </div>

                    <div className="form-group">
                        <label>Hora Fin</label>
                        <input
                            type="time"
                            name="horaFin"
                            value={formData.horaFin}
                            onChange={handleChange}
                            required
                            className="form-input"
                        />
                    </div>

                    <button type="submit" disabled={loading} className="btn btn-primary">
                        {loading ? 'Reservando...' : 'Reservar Turno'}
                    </button>
                </form>
            </div>
        </div>
    );
};
