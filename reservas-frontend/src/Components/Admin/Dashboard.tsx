import React, { useState, useEffect } from 'react';
import { empleadosApi } from '../../API/empleados.api';
import { serviciosApi } from '../../API/servicios.api';
import { Empleado, Servicio } from '../../Types';
import './Admin.css';

export const Dashboard: React.FC = () => {
    const [activeTab, setActiveTab] = useState<'empleados' | 'servicios'>('empleados');
    const [empleados, setEmpleados] = useState<Empleado[]>([]);
    const [servicios, setServicios] = useState<Servicio[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    // Formularios
    const [showFormEmpleado, setShowFormEmpleado] = useState(false);
    const [showFormServicio, setShowFormServicio] = useState(false);

    const [formEmpleado, setFormEmpleado] = useState({
        nombreCompleto: '',
        email: '',
        telefono: '',
    });

    const [formServicio, setFormServicio] = useState({
        nombre: '',
        descripcion: '',
        duracion: '',
        precio: '',
    });

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
            setError('Error al cargar datos');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        cargarDatos();
    }, []);

    const handleSubmitEmpleado = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            setLoading(true);
            await empleadosApi.crear({
                Nombre_Completo: formEmpleado.nombreCompleto,
                Email: formEmpleado.email,
                Telefono: formEmpleado.telefono,
            });
            setFormEmpleado({ nombreCompleto: '', email: '', telefono: '' });
            setShowFormEmpleado(false);
            await cargarDatos();
        } catch (err: any) {
            setError(err.response?.data?.message || 'Error al crear empleado');
        } finally {
            setLoading(false);
        }
    };

    const handleSubmitServicio = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            setLoading(true);
            await serviciosApi.crear({
                Nombre: formServicio.nombre,
                Descripcion: formServicio.descripcion,
                Duracion_Minutos: parseInt(formServicio.duracion),
                Precio: parseFloat(formServicio.precio),
            });
            setFormServicio({ nombre: '', descripcion: '', duracion: '', precio: '' });
            setShowFormServicio(false);
            await cargarDatos();
        } catch (err: any) {
            setError(err.response?.data?.message || 'Error al crear servicio');
        } finally {
            setLoading(false);
        }
    };

    const eliminarEmpleado = async (id: number) => {
        if (!window.confirm('¿Estás seguro?')) return;
        try {
            await empleadosApi.eliminar(id);
            setEmpleados(empleados.filter(e => e.Id_Empleados !== id));
        } catch (err) {
            setError('Error al eliminar empleado');
        }
    };

    const eliminarServicio = async (id: number) => {
        if (!window.confirm('¿Estás seguro?')) return;
        try {
            await serviciosApi.eliminar(id);
            setServicios(servicios.filter(s => s.Id_Servicios !== id));
        } catch (err) {
            setError('Error al eliminar servicio');
        }
    };

    return (
        <div className="container">
            <div className="card">
                <h2>Panel de Administración</h2>

                {error && <div className="alert alert-error">{error}</div>}

                <div className="tabs">
                    <button
                        className={`tab ${activeTab === 'empleados' ? 'active' : ''}`}
                        onClick={() => setActiveTab('empleados')}
                    >
                        Empleados
                    </button>
                    <button
                        className={`tab ${activeTab === 'servicios' ? 'active' : ''}`}
                        onClick={() => setActiveTab('servicios')}
                    >
                        Servicios
                    </button>
                </div>

                {activeTab === 'empleados' && (
                    <div className="tab-content">
                        <button
                            onClick={() => setShowFormEmpleado(!showFormEmpleado)}
                            className="btn btn-primary"
                        >
                            {showFormEmpleado ? 'Cancelar' : 'Nuevo Empleado'}
                        </button>

                        {showFormEmpleado && (
                            <form onSubmit={handleSubmitEmpleado} className="form">
                                <div className="form-group">
                                    <label>Nombre Completo</label>
                                    <input
                                        type="text"
                                        value={formEmpleado.nombreCompleto}
                                        onChange={(e) =>
                                            setFormEmpleado({ ...formEmpleado, nombreCompleto: e.target.value })
                                        }
                                        required
                                        className="form-input"
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Email</label>
                                    <input
                                        type="email"
                                        value={formEmpleado.email}
                                        onChange={(e) =>
                                            setFormEmpleado({ ...formEmpleado, email: e.target.value })
                                        }
                                        required
                                        className="form-input"
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Teléfono</label>
                                    <input
                                        type="tel"
                                        value={formEmpleado.telefono}
                                        onChange={(e) =>
                                            setFormEmpleado({ ...formEmpleado, telefono: e.target.value })
                                        }
                                        className="form-input"
                                    />
                                </div>
                                <button type="submit" disabled={loading} className="btn btn-primary">
                                    {loading ? 'Guardando...' : 'Crear Empleado'}
                                </button>
                            </form>
                        )}

                        <div className="items-list">
                            {empleados.map((emp) => (
                                <div key={emp.Id_Empleados} className="item">
                                    <div>
                                        <h4>{emp.NombreCompleto}</h4>
                                        <p>{emp.Email}</p>
                                        <p>{emp.Telefono}</p>
                                    </div>
                                    <button
                                        onClick={() => eliminarEmpleado(emp.Id_Empleados)}
                                        className="btn btn-danger btn-sm"
                                    >
                                        Eliminar
                                    </button>
                                </div>
                            ))}
                        </div>
                    </div>
                )}

                {activeTab === 'servicios' && (
                    <div className="tab-content">
                        <button
                            onClick={() => setShowFormServicio(!showFormServicio)}
                            className="btn btn-primary"
                        >
                            {showFormServicio ? 'Cancelar' : 'Nuevo Servicio'}
                        </button>

                        {showFormServicio && (
                            <form onSubmit={handleSubmitServicio} className="form">
                                <div className="form-group">
                                    <label>Nombre</label>
                                    <input
                                        type="text"
                                        value={formServicio.nombre}
                                        onChange={(e) =>
                                            setFormServicio({ ...formServicio, nombre: e.target.value })
                                        }
                                        required
                                        className="form-input"
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Descripción</label>
                                    <input
                                        type="text"
                                        value={formServicio.descripcion}
                                        onChange={(e) =>
                                            setFormServicio({ ...formServicio, descripcion: e.target.value })
                                        }
                                        className="form-input"
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Duración (minutos)</label>
                                    <input
                                        type="number"
                                        value={formServicio.duracion}
                                        onChange={(e) =>
                                            setFormServicio({ ...formServicio, duracion: e.target.value })
                                        }
                                        required
                                        className="form-input"
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Precio</label>
                                    <input
                                        type="number"
                                        step="0.01"
                                        value={formServicio.precio}
                                        onChange={(e) =>
                                            setFormServicio({ ...formServicio, precio: e.target.value })
                                        }
                                        required
                                        className="form-input"
                                    />
                                </div>
                                <button type="submit" disabled={loading} className="btn btn-primary">
                                    {loading ? 'Guardando...' : 'Crear Servicio'}
                                </button>
                            </form>
                        )}

                        <div className="items-list">
                            {servicios.map((srv) => (
                                <div key={srv.Id_Servicios} className="item">
                                    <div>
                                        <h4>{srv.Nombre}</h4>
                                        <p>{srv.Descripcion}</p>
                                        <p>Duración: {srv.Duracion_Minutos} min | Precio: ${srv.Precio}</p>
                                    </div>
                                    <button
                                        onClick={() => eliminarServicio(srv.Id_Servicios)}
                                        className="btn btn-danger btn-sm"
                                    >
                                        Eliminar
                                    </button>
                                </div>
                            ))}
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};
