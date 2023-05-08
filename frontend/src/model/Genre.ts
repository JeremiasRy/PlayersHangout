import { GenericState } from "../redux/Slice/genericSlice";
import { BaseModel } from "./BaseModel";

export interface Genre extends BaseModel {}

export interface GenreState extends GenericState<Genre> {}

export const genreEmpty: Genre = {
    id: '',
    name: ''
}

export const genreState: GenreState = {
    data: genreEmpty,
    status: 'loading'
}