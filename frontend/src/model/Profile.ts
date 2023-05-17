import { User } from "./User";

export interface Profile extends User {
    token: string,
    expiration: string
}

export interface ProfileState {
    data?: Profile,
    fetching: boolean
}