// import axios, { AxiosResponse, InternalAxiosRequestConfig } from "axios";

// const api = axios.create({
//     baseURL: 'https://localhost:7267/api/v1/'
// });

// class HttpError extends Error {
//     constructor(message?: string) {
//       super(message); // 'Error' breaks prototype chain here
//       this.name = 'HttpError';
//       Object.setPrototypeOf(this, new.target.prototype); // restore prototype chain
//     }
// }
  
// function responseHandler(response: AxiosResponse<any>): any {
//     if (response.status == 200) {
//       const data = response?.data;
//       if (!data) throw new HttpError('API Error. No data!');
//       return response;
//     }
//     throw new HttpError('API Error! Invalid status code!');
// }

// function requestHandler(config: InternalAxiosRequestConfig<any>) {
//     try {
//       const token = JSON.parse(localStorage.getItem('token') ?? '');
//       config.headers.Authorization = `Bearer ${token}`;
//       return config;
//     } catch (error) {
//       config.headers.Authorization = undefined;
//     }
//     return config;
// }

// function errorHandler(error: any) {
//     return Promise.reject(error);
// }

// api.interceptors.response.use(responseHandler);
// api.interceptors.request.use(requestHandler, errorHandler);

// export default api;
  