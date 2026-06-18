import { BrowserRouter, Routes, Route } from "react-router-dom"

import 'bootstrap/dist/css/bootstrap.min.css'
import LoginPage from './pages/login-page'
import MainPage from "./pages/main-page"
import useMessage from "./hooks/useMessage"
import MainListComponent from "./components/main-list-component"
import SelectTagComponent from "./components/select-tag-component"
import EditBlogComponent from "./components/edit-blog-component"
import ErrorPage from "./pages/error-page"

function App() {

  const {MessageComponent} = useMessage();

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage/>}></Route>
          {/*  */}
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
