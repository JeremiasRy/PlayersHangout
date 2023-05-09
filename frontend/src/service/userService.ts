import { CreateUser, LoginUser } from "../api/request/user";
import { AuthResponse } from "../api/response/user";
import { post } from "./genericService";

const userRegister = post<CreateUser, AuthResponse>('auths/signup', 'Signup');
const userLogin = post<LoginUser, AuthResponse>('auths/login', 'Signup');

export {
    userRegister,
    userLogin
}