import { Genre } from "../../model/Genre";
import { BaseCrudSlice } from "./BaseCrudSlice";

export const genreSlice = new BaseCrudSlice<Genre, Genre, Genre>('GenreSlice', 'Genres');
export const { getAllAsync: getAllGenre, getOneAsync: getOneGenre, createAsync: createGenre, removeAsync: RemoveGenre } = genreSlice;