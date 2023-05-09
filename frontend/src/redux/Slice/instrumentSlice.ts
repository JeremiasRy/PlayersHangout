import { instrumentState } from "../../model/Instrument";
import { genericSlice } from "./genericSlice";

export const instrumentSlice = genericSlice({
    name: 'instrumentSlice', 
    initialState: instrumentState,
    reducers: {}
})