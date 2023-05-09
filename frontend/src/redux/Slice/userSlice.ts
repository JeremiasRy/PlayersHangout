import { ActionReducerMapBuilder, createSlice } from "@reduxjs/toolkit";
import { User, UserState, userState } from "../../model/User";
import { genericSlice } from "./genericSlice";
import { userRegister } from "../../service/userService";

export const userSlice = genericSlice<User, {}>({
    name: 'userSlice', 
    initialState: userState,
    reducers: {},
    extraReducers: (builder) => {
        builder
          .addCase(userRegister.fulfilled, (state: UserState, action) => {
            // TODO This is just a test
            console.log("userRegister fulfilled action,", action)
            state.status = 'loading';            
          })
      }
})