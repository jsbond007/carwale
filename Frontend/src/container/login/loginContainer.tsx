import React, { useState, useEffect } from 'react'
import Login from '../../component/login/login'
import { LoginAPI } from '../../service/loginService';
import { toast } from "react-toastify";
import { useNavigate } from 'react-router-dom';

export default function LoginContainer() {

    const [username, setUserName] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    useEffect(() => {
        if (localStorage.getItem("token")) {
            navigate("/");
        }
    })

    const handleInput = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        let name = e.target.name;
        let value = e.target.value;
        if (name == "username")
            setUserName(value);
        else if (name == "password")
            setPassword(value);
    };

    const handleLogin = async () => {
        if (username && password) {
            const res = await LoginAPI(username, password);
            if (res.data.data && res.data.data.token) {
                localStorage.setItem("token", res.data.data.token);
                navigate("/");
                toast.success("Login successful");
            }
        }
        else {
            toast.error(" UserName and Password are both required");
        }


    }

    return (
        <div>
            <Login handleInput={handleInput}
                handleLogin={handleLogin} />
        </div>
    )
}
