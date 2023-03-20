import React, { ChangeEvent } from 'react'
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import CloseIcon from '@mui/icons-material/Close';
import { useNavigate } from 'react-router-dom';
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import { Link } from 'react-router-dom';
import classes from "./login.module.scss"
import CImage2 from "../../asset/images/CImage2.jpeg";
import CImage1 from "../../asset/images/CImage1.jpeg";
import CImage4 from "../../asset/images/CImage4.jpeg";
import CImage3 from "../../asset/images/CImage3.jpeg";
import { FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput } from '@mui/material';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import Visibility from '@mui/icons-material/Visibility';
import { styled } from "@mui/material/styles";

type Props = {
    handleInput: (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void;
    handleLogin: () => void;
}

const CssTextField = styled(TextField)({
    "& label.Mui-focused": {
      color: "#464775",
    },
    "& .MuiOutlinedInput-root": {
      "&.Mui-focused fieldset": {
        borderColor: "#464775",
      },
    },
  });

export default function Login({ handleInput, handleLogin }: Props) {

    const [showPassword, setShowPassword] = React.useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };
    return (
        <div className={classes.right_side_div}>
            <div className={classes.login_area_div}>
                <div className={classes.left_background_img}>
                    <div className={classes.background_img_car}>
                        <div className={classes.car1}> <img src={CImage1} alt="car" /></div>
                        <div className={classes.car2}><img src={CImage4} alt="car" /></div>
                    </div>
                    <div className={classes.car4}>
                        <img src={CImage3} alt="car" />
                    </div>
                </div>
                <div className={classes.login_form_container}>

                    <div className={classes.login_form_input}>
                        <div className={classes.login_form_header_first}>
                            <div className={classes.car_image}>
                                <img src={CImage2} alt="car" />

                            </div>
                            <div className={classes.title_header}>CarWale</div>
                        </div>
                        <div className={classes.login_form_header}>

                            <div className={classes.login_form_header_1}>Login To Access Your Panel</div>
                        </div>
                    </div>

                    <div className={classes.text_field_div_1}>
                        <Box
                            component="form"
                            sx={{ "& > :not(style)": { m: 1, width: "90%" } }}
                            noValidate
                            autoComplete="off"
                        >
                            <CssTextField
                                id="outlined-basic"
                                name="username"
                                label="Username"
                                variant="outlined"
                                onChange={(e) => handleInput(e)}
                            />
                        </Box>

                    </div>

                    <div className={classes.text_field_div_2}>
                        <Box
                            component="form"
                            sx={{
                                "& > :not(style)": { m: 1, width: "90%" },
                                position: "relative",
                            }}
                            noValidate
                            autoComplete="off"
                        >
                            <FormControl>
                                {/* <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel> */}
                                <CssTextField
                                    id="outlined-basic"
                                    type={showPassword ? 'text' : 'password'}
                                    onChange={(e) => handleInput(e)}
                                    name="password"
                                    label="Password"
                                    // endAdornment={
                                    //     <InputAdornment position="end">
                                    //         <IconButton
                                    //             aria-label="toggle password visibility"
                                    //             onClick={handleClickShowPassword}
                                    //             onMouseDown={handleMouseDownPassword}
                                    //             edge="end"
                                    //         >
                                    //             {showPassword ? <VisibilityOff /> : <Visibility />}
                                    //         </IconButton>
                                    //     </InputAdornment>
                                    // }

                                    InputProps={{
                                        endAdornment: (
                                          <InputAdornment position="end">
                                            <IconButton
                                                aria-label="toggle password visibility"
                                                onClick={handleClickShowPassword}
                                                onMouseDown={handleMouseDownPassword}
                                                // edge="end"
                                            >
                                                {showPassword ? <VisibilityOff /> : <Visibility />}
                                            </IconButton>
                                          </InputAdornment>
                                        ),
                                      }}
                                    
                                />
                            </FormControl>

                        </Box>
                    </div>
                    <div className={classes.login_button}>
                        <button onClick={() => handleLogin()}>Login</button>
                    </div>
                </div>
            </div>
        </div>
    )
}
