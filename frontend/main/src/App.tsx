//Libraries
import { Routes, Route, BrowserRouter} from "react-router-dom"
import {createContext} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'
//modules
import LoginPage from './pages/login-page'
import RegisterPage from './pages/register-page'
import MainPage from "./pages/main-page"
import MainListComponent from "./components/main-list-component"
import SelectTagComponent from "./components/select-tag-component"
import EditBlogComponent from "./components/edit-blog-component"
import ErrorPage from "./pages/error-page"
import NotFoundPage from "./pages/not-found-page"
import RegisterFormComponent from "./components/register-form-component"
import VerifyEmailFormComponent from "./components/verify-email-form-component"
import ImplementRegisterComponent from "./components/implement-register-component"
import ForgotPassPage from "./pages/forgot-pass-page"
import FgPassEmailFormComponent from "./components/fg-pass-email-form-component"
import FgPassVrfEmailFormComponent from "./components/fg-pass-vrf-email-form-component"
import FgPassNewPassFormComponent from "./components/fg-pass-new-pass-form-component"
import ProfilePage from "./pages/profile-page"
import BlogDetailComponent from "./components/blog-details-component"
import useAuth from "./hooks/useAuth"

const AuthContext = createContext<ReturnType<typeof useAuth> | null>(null);
  
function App() {
  const auth = useAuth();

  if(window.location.pathname === '/'){
    window.location.href = '/login';
  }

  return (
    <>
      <AuthContext.Provider value={auth}>
        <BrowserRouter>
          <Routes>
            <Route path="/login" element={<LoginPage/>}></Route>
            <Route path="/register" element={<RegisterPage/>}>
                <Route path="" element={<RegisterFormComponent/>}></Route>
                <Route path="verify-email" element={<VerifyEmailFormComponent/>}></Route>
                <Route path="implement-register" element={<ImplementRegisterComponent/>}></Route>
            </Route>
            <Route path="/forgot-password" element={<ForgotPassPage/>}>
                <Route path="" element={<FgPassEmailFormComponent />}></Route>
                <Route path="verify-email" element={<FgPassVrfEmailFormComponent />}></Route>
                <Route path="new-password" element={<FgPassNewPassFormComponent />}></Route>
            </Route>
            <Route path="/blog" element={<MainPage/>}>
              <Route path="" element={<MainListComponent />}>
                <Route path="new" element={<MainListComponent />}></Route>
                <Route path="top" element={<MainListComponent />}></Route>
              </Route>  
              <Route path="tags" element={<SelectTagComponent />}></Route>
              <Route path="edit" element={<EditBlogComponent />}></Route>
              <Route path="details" element={<BlogDetailComponent />}></Route>
            </Route>
            <Route path="error" element={<ErrorPage/>}></Route>
            <Route path="/profile" element={<ProfilePage/>}></Route>

            // 404 page
            <Route path="*" element={<NotFoundPage/>}></Route>
          </Routes>
        </BrowserRouter>
      </AuthContext.Provider>
    </>
  )
}

export default App
export {AuthContext};
