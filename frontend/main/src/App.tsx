import { BrowserRouter, Routes, Route } from "react-router-dom"

import 'bootstrap/dist/css/bootstrap.min.css'
import LoginPage from './pages/login'
import MainPage from "./pages/main"
import useMessage from "./hooks/useMessage"

function App() {

  const {MessageComponent} = useMessage();

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage/>}></Route>
          {/*  */}
          <Route path="/blog" element={<MainPage/>}></Route>
        </Routes>
      </BrowserRouter>

      <MessageComponent/>
    </>
  )
}

export default App
