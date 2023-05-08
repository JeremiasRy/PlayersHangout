import { ActionReducerMapBuilder, CaseReducer, CreateSliceOptions, PayloadAction, SliceCaseReducers, ValidateSliceCaseReducers, createSlice } from "@reduxjs/toolkit"

export interface GenericState<T> {
    data: T,
    status: 'loading' | 'success' | 'error'
}

export const genericSlice = <T, Reducers extends SliceCaseReducers<GenericState<T>>>({
    name = '',
    initialState,
    reducers,
    extraReducers
} : {
    name: string,
    initialState: GenericState<T>,
    reducers: ValidateSliceCaseReducers<GenericState<T>, Reducers>,
    extraReducers:  (builder: ActionReducerMapBuilder<GenericState<T>>) => void
}) => {
    return createSlice({
        name,
        initialState,
        reducers: {
            start(state: GenericState<T>) {
                state.status = 'loading'
            },
            success(state: GenericState<T>, action: PayloadAction<T>) {
                state.data = action.payload;
                state.status = 'success';
            },
            ...reducers
        },
        extraReducers
    })
} 