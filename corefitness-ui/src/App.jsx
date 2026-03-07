import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Home from './pages/Home'
import Memberships from './pages/Memberships'
import CustomerService from './pages/CustomerService'
import AccountAboutMe from './pages/AccountAboutMe'
import SignIn from './pages/SignIn'
import SignUp from './pages/SignUp'
import SetPassword from './pages/SetPassword'
import NotFound from './pages/NotFound'

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/memberships" element={<Memberships />} />
        <Route path="/customer-service" element={<CustomerService />} />
        <Route path="/account" element={<AccountAboutMe />} />
        <Route path="/signin" element={<SignIn />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/set-password" element={<SetPassword />} />
        <Route path="*" element={<NotFound />} />

      </Routes>
    </BrowserRouter>
  )
}
