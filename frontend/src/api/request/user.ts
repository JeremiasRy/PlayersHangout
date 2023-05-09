import { User } from "../../model/User";

export interface CreateUser extends Omit<User, 'id'> {
    password: string
}

export interface LoginUser {
    email: string;
    password: string;
}