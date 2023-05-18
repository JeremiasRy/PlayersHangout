import { Draft, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { BaseModel } from "../../model/BaseModel";
import axios, { AxiosError, AxiosResponse, InternalAxiosRequestConfig } from "axios";

export interface SliceState<T extends BaseModel> {
    data: T[];
    fetching: boolean;
}

export interface ActionUrl {
    action?: string
}

// Put Auth Token if we have it
axios.interceptors.request.use((config) => {
    try {
        const token = JSON.parse(localStorage.getItem('token') ?? '');
        config.headers.Authorization = `Bearer ${token}`;
        return config;
      } catch (error) {
        config.headers.Authorization = undefined;
      }
      return config;
}) 

export class BaseCrudSlice<T extends BaseModel, TCreate, TUpdate extends BaseModel> {

    constructor(name: string, endpoint: string) {
        this.name = name;
        // To get the url from .env file
        this.baseUrl = import.meta.env.VITE_BASE_URL ?? "";
        this.endpoint = endpoint;
        this.slice = createSlice({
            name: this.name,
            initialState: this.initialState,
            reducers: {},
            extraReducers: (build) => {
                /** pending */
                build.addCase(this.getAllAsync.pending, (state, action) => {   
                    const { payload } = action;
                    if (payload) {
                        return { data: payload, fetching: true }
                    }                             
                }),
                /** fulfilled */
                build.addCase(this.getAllAsync.fulfilled, (_, action) => {                                
                    return { data: action.payload, fetching: false }
                }),
                build.addCase(this.createAsync.fulfilled, (state, action) => {                                             
                    const { payload } = action;
                    return { data: [...state.data, payload as Draft<T>], fetching: false}                    
                }),
                build.addCase(this.updateAsync.fulfilled, (state, action) => {                                             
                    const { payload } = action;
                    if (payload) {
                        const elemnents = [...state.data];
                        const index = elemnents.findIndex((item) => item.id === payload.id);
                        if (index !== -1) {
                            elemnents[index] = payload as Draft<T>;
                        }
                        return { data: elemnents, fetching: false}
                    }
                }),
                build.addCase(this.removeAsync.fulfilled, (state, action) => {                            
                    const { payload } = action;
                    if (payload) {                                                
                        return { data: state.data.filter(item => item.id !== payload), fetching: false}                                 
                    }                  
                })
            }
        });

        this.getAllAsync = createAsyncThunk<T[], {}, { rejectValue: AxiosError }>(
            `getAll ${this.endpoint}`,
            async (request: {}, thunkAPI) => {
                try {
                    const result = await axios.get(
                   `${this.baseUrl}/${this.endpoint}`, 
                    {
                        params: { ...request },
                    });
                    return thunkAPI.fulfillWithValue(result.data) ;
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im get all... sorry!");   
                }
            }
        );

        this.getOneAsync = createAsyncThunk<T, number, { rejectValue: AxiosError }>(
            `getById ${this.endpoint}`,
            async (id:number, thunkAPI) => {
                try {
                    const result = await axios.get(`${this.baseUrl}/${this.endpoint}/${id}`)
                    return thunkAPI.fulfillWithValue(result.data)                    
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im get one... sorry!");   
                }
            }
        );

        this.createAsync = createAsyncThunk<T | undefined, TCreate, {rejectValue: AxiosError}>(
            `create ${this.endpoint}`,
            async (create:TCreate, thunkAPI) => {
                try {
                    const result = await axios.post<any, AxiosResponse<T>, TCreate>(
                        `${this.baseUrl}/${this.endpoint}`,
                        create
                    )
                    return thunkAPI.fulfillWithValue(result.data);
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im Create... sorry!");                                    
                }
            }
        );

        this.updateAsync = createAsyncThunk<T | undefined, TUpdate, { rejectValue: AxiosError }>(
            `update ${this.endpoint}`,
            async (update: TUpdate, thunkAPI) => {
                try {
                    const result = await axios.put<T>(
                        `${this.baseUrl}/${this.endpoint}/${update.id}`,
                        update
                    )
                    return thunkAPI.fulfillWithValue(result.data);
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im update... sorry!");   
                }
            }
        );

        this.removeAsync = createAsyncThunk<string | undefined, string, { rejectValue: AxiosError }>(
            `delete ${this.endpoint}`,
            async (id: string, thunkAPI) => {
                try {
                    const result = await axios.delete<boolean>(`${this.baseUrl}/${this.endpoint}/${id}`);
                    return thunkAPI.fulfillWithValue(result.data ? id : '');                  
                } catch (error) {
                    if (axios.isAxiosError(error)) return thunkAPI.rejectWithValue(error as AxiosError)
                    console.error("Something went horribly wrong im remove... sorry!");   
                }
            }
        );
    };

    slice:ReturnType<typeof createSlice<SliceState<T>, {}, string>>;
    private initialState:SliceState<T> = { data: [], fetching: false }
    name: string
    baseUrl: string
    endpoint: string         
    getAllAsync: ReturnType<typeof createAsyncThunk<T[], {}, { rejectValue: AxiosError }>>;
    getOneAsync: ReturnType<typeof createAsyncThunk<T, number, { rejectValue: AxiosError }>>;
    createAsync: ReturnType<typeof createAsyncThunk<T | undefined, TCreate, { rejectValue: AxiosError }>>;
    updateAsync: ReturnType<typeof createAsyncThunk<T | undefined, TUpdate, { rejectValue: AxiosError }>>;
    removeAsync: ReturnType<typeof createAsyncThunk<string | undefined, string, { rejectValue: AxiosError }>>;
}