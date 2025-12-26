import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { authApi } from '../../API/auth.api';
import './Auth.css';

export const Register: React.FC = () => {
    const [formData, setFormData] = useState({
        nombreCompleto: '',
        email: '',
        password: '',
        confirmPassword: '',
    });
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');

        // Validar que los campos no estén vacíos
        if (!formData.nombreCompleto.trim()) {
            setError('El nombre completo es requerido');
            return;
        }

        if (!formData.email.trim()) {
            setError('El email es requerido');
            return;
        }

        if (!formData.password) {
            setError('La contraseña es requerida');
            return;
        }

        if (!formData.confirmPassword) {
            setError('La confirmación de contraseña es requerida');
            return;
        }

        // Comparar contraseñas (trim para eliminar espacios)
        const pass1 = formData.password.trim();
        const pass2 = formData.confirmPassword.trim();
        
        if (pass1 !== pass2) {
            setError(`Las contraseñas no coinciden. Contraseña 1: "${pass1}" vs Contraseña 2: "${pass2}"`);
            return;
        }

        if (formData.password.length < 6) {
            setError('La contraseña debe tener al menos 6 caracteres');
            return;
        }

        setLoading(true);

        try {
            await authApi.register({
                NombreCompleto: formData.nombreCompleto,
                Email: formData.email,
                Password: formData.password,
            });
            navigate('/login');
        } catch (err: any) {
            setError(err.response?.data?.message || 'Error al registrarse');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="auth-container">
            <div className="auth-card">
                <h2 className="auth-title">Crear Cuenta</h2>
                
                {error && <div className="alert alert-error">{error}</div>}

                <form onSubmit={handleSubmit} className="auth-form">
                    <div className="form-group">
                        <label className="form-label">Nombre Completo</label>
                        <input
                            type="text"
                            name="nombreCompleto"
                            value={formData.nombreCompleto}
                            onChange={handleChange}
                            required
                            className="form-input"
                            placeholder="Juan Pérez"
                        />
                    </div>

                    <div className="form-group">
                        <label className="form-label">Email</label>
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                            className="form-input"
                            placeholder="tu@email.com"
                        />
                    </div>

                    <div className="form-group">
                        <label className="form-label">Contraseña</label>
                        <input
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                            className="form-input"
                            placeholder="••••••••"
                        />
                        <small style={{color: '#666', marginTop: '4px', display: 'block'}}>
                            Mínimo 6 caracteres
                        </small>
                    </div>

                    <div className="form-group">
                        <label className="form-label">Confirmar Contraseña</label>
                        <input
                            type="password"
                            name="confirmPassword"
                            value={formData.confirmPassword}
                            onChange={handleChange}
                            required
                            className="form-input"
                            placeholder="••••••••"
                        />
                        {formData.password && formData.confirmPassword && (
                            <small style={{
                                color: formData.password === formData.confirmPassword ? '#28a745' : '#dc3545',
                                marginTop: '4px',
                                display: 'block'
                            }}>
                                {formData.password === formData.confirmPassword 
                                    ? '✓ Las contraseñas coinciden' 
                                    : '✗ Las contraseñas no coinciden'}
                            </small>
                        )}
                    </div>

                    <button type="submit" disabled={loading} className="btn btn-primary">
                        {loading ? 'Registrando...' : 'Registrarse'}
                    </button>
                </form>

                <p className="auth-footer">
                    ¿Ya tienes cuenta? <Link to="/login" className="auth-link">Inicia sesión aquí</Link>
                </p>
            </div>
        </div>
    );
};