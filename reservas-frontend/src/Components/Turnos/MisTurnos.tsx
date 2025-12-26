import React, { useState, useEffect } from 'react';
import { useAuth } from '../../Context/AuthContext';
import { turnosApi } from '../../API/turnos.api';
import { Turno } from '../../Types';
import './Turnos.css';

export const MisTurnos: React.FC = () => {
    const { user } = useAuth();
    const [turnos, setTurnos] = useState<Turno[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    useEffect(() => {
        cargarTurnos();
    }, []); // eslint-disable-next-line react-hooks/exhaustive-deps

    const cargarTurnos = async () => {
        try {
            setLoading(true);
            setError('');
            // Para cargar turnos del usuario actual, necesitarías un endpoint específico
            // Por ahora, mostramos un mensaje informativo
            console.log('Usuario:', user);
            setTurnos([]);
        } catch (err: any) {
            setError(err.response?.data?.message || 'Error al cargar turnos');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    const cancelarTurno = async (id: number) => {
        if (!window.confirm('¿Estás seguro de que quieres cancelar este turno?')) return;

        try {
            await turnosApi.cancelar(id);
            setTurnos(turnos.filter(t => t.Id_Turno !== id));
        } catch (err: any) {
            setError(err.response?.data?.message || 'Error al cancelar turno');
        }
    };

    return (
        <div className="container">
            <div className="card">
                <h2>Mis Turnos</h2>

                {error && <div className="alert alert-error">{error}</div>}

                {loading && <p>Cargando turnos...</p>}

                {!loading && turnos.length === 0 && (
                    <div className="info-message">
                        <p>No tienes turnos reservados aún.</p>
                        <p>Ve a "Reservar Turno" para crear uno nuevo.</p>
                    </div>
                )}

                {!loading && turnos.length > 0 && (
                    <div className="turnos-table">
                        <table>
                            <thead>
                                <tr>
                                    <th>Servicio</th>
                                    <th>Empleado</th>
                                    <th>Fecha</th>
                                    <th>Hora</th>
                                    <th>Estado</th>
                                    <th>Acción</th>
                                </tr>
                            </thead>
                            <tbody>
                                {turnos.map((turno) => (
                                    <tr key={turno.Id_Turno}>
                                        <td>{turno.ServicioNombre}</td>
                                        <td>{turno.EmpleadoNombre}</td>
                                        <td>{new Date(turno.Fecha).toLocaleDateString()}</td>
                                        <td>{turno.HoraInicio} - {turno.HoraFin}</td>
                                        <td>
                                            <span className={`estado ${turno.Estado.toLowerCase()}`}>
                                                {turno.Estado}
                                            </span>
                                        </td>
                                        <td>
                                            {turno.Estado === 'Pendiente' && (
                                                <button
                                                    onClick={() => cancelarTurno(turno.Id_Turno)}
                                                    className="btn btn-danger btn-sm"
                                                >
                                                    Cancelar
                                                </button>
                                            )}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};
