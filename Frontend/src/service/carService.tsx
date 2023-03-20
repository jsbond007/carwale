import axios from "axios"
import { Environment } from "../shared/environment";


const baseURL = "/Car";
const url = Environment.getUrl() + baseURL;

export async function Get(uid: string) {
    return await axios.get(`${url}/${uid}`);
}

export async function GetAll(status?: number) {
    return await axios.get(`${url}?status=${status??''}`);
}

export async function AddCar(data: any) {
    return await axios.post(`${url}`, data);
}

export async function UpdateCar(data: any) {
    return await axios.put(`${url}`, data);
}

export async function DeleteCar(uid: string) {
    return await axios.delete(`${url}?uid=${uid}`);
}
