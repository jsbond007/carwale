import axios from 'axios';
import { Environment } from '../shared/environment';

const baseURL = "/Make";
const url = Environment.getUrl() + baseURL;

export async function getCarMakeList() {
    return await axios.get(`${url}`);
}

export async function getCarModel(makeUId: string) {
    return await axios.get(`${Environment.getUrl()}/make/${makeUId}/model`);
}   