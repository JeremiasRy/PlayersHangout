import { createAsyncThunk } from "@reduxjs/toolkit";
import api from "../api/axios";

export function getAll<T>(url: string, name: string) {
    return createAsyncThunk(
        name,
        async (thunkAPI) => {
            return api
                .get(url)
                .then((result) => {
                    return result.data as T[];
                })
                .catch((err) => {
                    console.error(`Error GetAll ${url} -> `, err);
                    return null;
                })
        }
    )
}

export function get<T>(url: string, name: string) {
    return createAsyncThunk(
        name,
        async (thunkAPI) => {
            return api
                .get(url)
                .then((result) => {
                    return result.data as T;
                })
                .catch((err) => {
                    console.error(`Error Get ${url} -> `, err);
                    return null;
                })
        }
    )
}

export function post<TCreate, TReturn>(url: string, name: string) {
    return createAsyncThunk(
        name,
        async (item: TCreate, thunkAPI) => {
            return api
                .post(url, item)
                .then((result) => {
                    console.log("En post function result ---> ", result)
                    return result.data as TReturn;
                })
                .catch((err) => {
                    console.error(`Error Create ${url} -> `, err);
                    return null;
                })
        }
    )
}

export function update<TUpdate, TReturn>(url: string, name: string) {
    return createAsyncThunk(
        name,
        async (item: TUpdate, thunkAPI) => {
            return api
                .put(url, item)
                .then((result) => {
                    return result.data as TReturn;
                })
                .catch((err) => {
                    console.error(`Error Update ${url} -> `, err);
                    return null;
                })
        }
    )
}

export function remove<T>(url: string, name: string) {
    return createAsyncThunk(
        name,
        async (thunkAPI) => {
            return api
                .delete(url)
                .then((result) => {
                    return result.data;
                })
                .catch((err) => {
                    console.error(`Error remove ${url} -> `, err);
                    return null;
                })
        }
    )
}