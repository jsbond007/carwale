import React, { Dispatch, SetStateAction } from 'react'
import { searchIcon } from '../../asset/commonSvg'
import classes from "./header.module.scss"
import CImage2 from "../../asset/images/CImage2.jpeg";
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import { useNavigate } from 'react-router-dom';
type AddEditModalProps = {
    onAddNew: () => void,
    totalCount: number,
    setStatus: Dispatch<SetStateAction<string>>,
}

export default function Header({ totalCount, onAddNew, setStatus }: AddEditModalProps) {
    const navigate = useNavigate();

    const handleChange = (event: SelectChangeEvent) => {
        setStatus(event.target.value.toString());
    };

    const onLogout = () => {
        localStorage.removeItem("token")
        navigate("/login");
    }

    return (
        <div>
            <div className={classes.container}>
                <div className={classes.header_part}>
                    <div className={classes.car_img}> <img src={CImage2} alt="car" /></div>
                    <div className={classes.logo_text}>CarWale</div>
                </div>

                <div className={classes.addDiv}>
                    <div className={classes.add_button} >
                        <div className={classes.button} onClick={() => onAddNew()}>
                            <div className={classes.button_pls}>+</div>
                            <div className={classes.button_add}> ADD NEW</div>
                        </div>

                    </div>
                    <div className={classes.total_count}>
                        Total {totalCount} cars
                    </div>
                    <div className={classes.dropdown}>

                        <FormControl sx={{ m: 1, minWidth: 120 }} size="small">
                            <InputLabel id="demo-select-small">All</InputLabel>
                            <Select
                                labelId="demo-select-small"
                                id="demo-select-small"
                                label="Age"
                                onChange={handleChange}
                            >
                                <MenuItem value="All">
                                    <em>All</em>
                                </MenuItem>
                                <MenuItem value={0}>Available</MenuItem>
                                <MenuItem value={1}>Unavailable</MenuItem>
                                <MenuItem value={2}>Stolen</MenuItem>
                                <MenuItem value={3}>Disposed</MenuItem>
                            </Select>
                        </FormControl>

                    </div>
                    <div className={classes.logOut_btn} onClick={() => onLogout()}>
                        Logout
                    </div>
                </div>
            </div>
        </div>
    )
}
