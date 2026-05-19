import { BrowserRouter, Routes, Route } from "react-router-dom"

import 'bootstrap/dist/css/bootstrap.min.css'
import LoginPage from './pages/login'
import MainPage from "./pages/main"
import useMessage from "./hooks/useMessage"
import MainListComponent from "./components/main-list-component"
import SelectCategComponent from "./components/select-categ-component"
import EditBlogComponent from "./components/edit-blog-component"

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
            <Route path="tags" element={<SelectCategComponent />}></Route>
            <Route path="edit" element={<EditBlogComponent />}></Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
