//Libraries
import { BrowserRouter, Routes, Route } from "react-router-dom"
import 'bootstrap/dist/css/bootstrap.min.css'
//modules
import LoginPage from './pages/login-page'
import RegisterPage from './pages/register-page'
import MainPage from "./pages/main-page"
import useMessage from "./hooks/useMessage"
import MainListComponent from "./components/main-list-component"
import SelectTagComponent from "./components/select-tag-component"
import EditBlogComponent from "./components/edit-blog-component"
import ErrorPage from "./pages/error-page"
import RegisterFormComponent from "./components/register-form-component"
import VerifyEmailFormComponent from "./components/verify-email-form-component"
import ImplementRegisterComponent from "./components/implement-register-component"

function App() {

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage/>}></Route>
          <Route path="/register" element={<RegisterPage/>}>
              <Route path="" element={<RegisterFormComponent/>}></Route>
              <Route path="verify-email" element={<VerifyEmailFormComponent/>}></Route>
              <Route path="implement-register" element={<ImplementRegisterComponent/>}></Route>
          </Route>
          <Route path="/blog" element={<MainPage/>}>
            <Route path="" element={<MainListComponent />}>
              <Route path="new" element={<MainListComponent />}></Route>
              <Route path="top" element={<MainListComponent />}></Route>
            </Route>
            <Route path="tags" element={<SelectTagComponent />}></Route>
            <Route path="edit" element={<EditBlogComponent />}></Route>
          </Route>
          <Route path="error" element={<ErrorPage/>}></Route>
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
