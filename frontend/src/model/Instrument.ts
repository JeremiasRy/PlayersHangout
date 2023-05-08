import { GenericState } from "../redux/Slice/genericSlice";
import { BaseModel } from "./BaseModel";

export interface Instrument extends BaseModel {}

export interface InstrumentState extends GenericState<Instrument> {}

const instrumentEmpty: Instrument = {
    id: '',
    name: ''
}

export const instrumentState: InstrumentState = {
    data: instrumentEmpty,
    status: 'loading'
}