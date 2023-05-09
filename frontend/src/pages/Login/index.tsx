import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import { userRegister } from "../../service/userService";
import { CreateUser } from "../../api/request/user";

function Login() {
    const dispatch = useAppDispatch();
    const userState = useAppSelector((state) => state.user);

    useEffect(() => {
        const user: CreateUser = {            
            name: "Daniel",
            lastName: "Moreno",
            email: "d@example.com",
            password: "Miramicuerpo000*",  
            city: "Malaga",
            latitude: 0,
            longitude: 0              
        }

        dispatch(userRegister(user))
    }, [userState])
    
    return (
        <div>
            Login
        </div>
    )
}

export default Login;