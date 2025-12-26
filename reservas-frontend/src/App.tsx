import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './Context/AuthContext';
import { Navbar } from './Components/Layout/Navbar';
import { Login } from './Components/Auth/Login';
import { Register } from './Components/Auth/Register';
import { ReservarTurno } from './Components/Turnos/ReservarTurno';
import { MisTurnos } from './Components/Turnos/MisTurnos';
import { Dashboard } from './Components/Admin/Dashboard';
import './App.css';

// Componente para rutas protegidas
const PrivateRoute: React.FC<{ children: React.ReactElement }> = ({ children }) => {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? children : <Navigate to="/login" />;
};

// Componente para la página de inicio
const Home: React.FC = () => (
  <div className="container">
    <div className="card">
      <h1>Bienvenido</h1>
      <p>Sistema de Reserva de Turnos</p>
      <p>Usa el menú superior para navegar y reservar tus turnos.</p>
    </div>
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
              <Home />
            </PrivateRoute>
          }
        />
        <Route
          path="/reservar"
          element={
            <PrivateRoute>
              <ReservarTurno />
            </PrivateRoute>
          }
        />
        <Route
          path="/mis-turnos"
          element={
            <PrivateRoute>
              <MisTurnos />
            </PrivateRoute>
          }
        />
        <Route
          path="/admin"
          element={
            <PrivateRoute>
              <Dashboard />
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
