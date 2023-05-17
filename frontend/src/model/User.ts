import { BaseModel } from "./BaseModel";

export interface User extends Omit<BaseModel, 'name'> {
    name: string;
    lastName: string;
    email: string;
    city: string,
    latitude: number,
    longitude: number
}

const userEmpty: User = {
    id: '',
    name: '',
    lastName: '',
    email: '',
    city: '',
    latitude: 0,
    longitude: 0
}