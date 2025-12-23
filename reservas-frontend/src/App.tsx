import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './Context/AuthContext';
import { Navbar } from './Components/Layout/Navbar';
import { Login } from './Components/Auth/Login';
import { Register } from './Components/Auth/Register';
import './App.css';

// Componente para rutas protegidas
const PrivateRoute: React.FC<{ children: React.ReactElement }> = ({ children }) => {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? children : <Navigate to="/login" />;
};

// Componente temporal para páginas en construcción
const ComingSoon: React.FC<{ title: string }> = ({ title }) => (
  <div className="coming-soon">
    <h1>{title}</h1>
    <p>Esta página estará disponible próximamente</p>
  </div>
);

function AppContent() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route
          path="/"
          element={
            <PrivateRoute>
              <ComingSoon title="Inicio" />
            </PrivateRoute>
          }
        />
        <Route
          path="/reservar"
          element={
            <PrivateRoute>
              <ComingSoon title="Reservar Turno" />
            </PrivateRoute>
          }
        />
        <Route
          path="/mis-turnos"
          element={
            <PrivateRoute>
              <ComingSoon title="Mis Turnos" />
            </PrivateRoute>
          }
        />
        <Route
          path="/admin"
          element={
            <PrivateRoute>
              <ComingSoon title="Panel de Administración" />
            </PrivateRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

function App() {
  return (
    <AuthProvider>
      <AppContent />
    </AuthProvider>
  );
}

export default App;
