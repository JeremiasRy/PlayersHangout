import { genreState } from "../../model/Genre";
import { genericSlice } from "./genericSlice";

export const genreSlice = genericSlice({
    name: 'userSlice', 
    initialState: genreState,
    reducers: {}
})