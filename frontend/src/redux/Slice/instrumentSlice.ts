import { Instrument } from "../../model/Instrument";
import { BaseCrudSlice } from "./BaseCrudSlice";

export const instrumentSlice = new BaseCrudSlice<Instrument, Instrument, Instrument>('InstrumentSlice', 'Instruments');
