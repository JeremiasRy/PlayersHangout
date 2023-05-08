import { Instrument } from "../model/Instrument";
import { getAll } from "./genericService";

const getAllInstruments = getAll<Instrument>('instruments', 'getAllInstruments');

export default {
    getAllInstruments
}