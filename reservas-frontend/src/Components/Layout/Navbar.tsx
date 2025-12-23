import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../../Context/AuthContext';
import './Navbar.css';

export const Navbar: React.FC = () => {
    const { user, logout, isAuthenticated, isAdmin } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <nav className="navbar">
            <div className="navbar-container">
                <Link to="/" className="brand">
                    Sistema de Reservas
                </Link>

                <div className="nav-menu">
                    {isAuthenticated ? (
                        <>
                            <Link to="/reservar" className="nav-link">
                                Nueva Reserva
                            </Link>
                            <Link to="/mis-turnos" className="nav-link">
                                Mis Turnos
                            </Link>
                            {isAdmin && (
                                <Link to="/admin" className="nav-link">
                                    Administración
                                </Link>
                            )}
                            <div className="user-info">
                                <span className="user-name">{user?.Nombre_Completo}</span>
                                <button onClick={handleLogout} className="logout-btn">
                                    Cerrar Sesión
                                </button>
                            </div>
                        </>
                    ) : (
                        <>
                            <Link to="/login" className="nav-link">
                                Iniciar Sesión
                            </Link>
                            <Link to="/register" className="nav-link">
                                Registrarse
                            </Link>
                        </>
                    )}
                </div>
            </div>
        </nav>
    );
};