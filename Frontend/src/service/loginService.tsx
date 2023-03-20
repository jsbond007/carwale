import axios from "axios";
import { Environment } from "../shared/environment";

const baseURL = "/auth";
const url = Environment.getUrl() + baseURL;

export async function LoginAPI(username: string, password: string) {
    let data = {
        userName: username,
        password: password
    };
    return await axios.post(`${url}/login`, data);
}