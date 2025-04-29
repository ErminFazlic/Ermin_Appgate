import { Route, Routes } from 'react-router-dom'
import './App.css'
import Login from './Login'
import Register from './Register'

function App() {

  return (
    <>
          <Routes>
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
          </Routes>
    </>
  )
}

export default App
