import { Action, ThunkAction, configureStore } from "@reduxjs/toolkit";
import { userSlice } from "./Slice/userSlice";
import { instrumentSlice } from "./Slice/instrumentSlice";
import { genreSlice } from "./Slice/genreSlice";



export const store = configureStore({
    reducer: {
        user: userSlice.reducer,
        instrument: instrumentSlice.reducer,
        genre: genreSlice.reducer
    }
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;
