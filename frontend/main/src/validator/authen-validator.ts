import type { ChangePasswordReqDTO, RegisterReqDTO } from "../models/generated-interfaces";

export class AuthenValidator
{
    private static ERROR_MESSAGES = {
        USERNAME_REQUIRED: "Username is required",
        PASSWORD_REQUIRED: "Password is required",
        REPASSWORD_REQUIRED: "Repeat password is required",
        FULLNAME_REQUIRED: "Full name is required",
        EMAIL_REQUIRED: "Email is required",
        INVALID_EMAIL: "Invalid email format",
        PASSWORDS_DO_NOT_MATCH: "Passwords do not match"
    };

    public static validateLoginRequest = (login: {username?: string, password?: string}): [boolean, string?] => {
        let message: string = "";
        if (!login) {
            message = this.ERROR_MESSAGES.USERNAME_REQUIRED;
            return [false, message];
        }
        
        // Validate required fields
        if (!login.username || login.username.trim() === "") {
            message = this.ERROR_MESSAGES.USERNAME_REQUIRED;
            return [false, message];
        }
        if (!login.password || login.password.trim() === "") {
            message = this.ERROR_MESSAGES.PASSWORD_REQUIRED;
            return [false, message];
        }
        
        return [true];
    }

    public static validateRegisterRequest = (register: RegisterReqDTO): [boolean, string?] => {
        let message: string = "";
        if (!register) {
            message = this.ERROR_MESSAGES.USERNAME_REQUIRED;
            return [false, message];
        }
        
        // Validate required fields
        if (!register.username || register.username.trim() === "") {
            message = this.ERROR_MESSAGES.USERNAME_REQUIRED;
            return [false, message];
        }
        if (!register.password || register.password.trim() === "") {
            message = this.ERROR_MESSAGES.PASSWORD_REQUIRED;
            return [false, message];
        }
        if (!register.repassword || register.repassword.trim() === "") {
            message = this.ERROR_MESSAGES.REPASSWORD_REQUIRED;
            return [false, message];
        }
        if (!register.fullname || register.fullname.trim() === "") {
            message = this.ERROR_MESSAGES.FULLNAME_REQUIRED;
            return [false, message];
        }
        if (!register.email || register.email.trim() === "") {
            message = this.ERROR_MESSAGES.EMAIL_REQUIRED;
            return [false, message];
        }
        
        // Validate email format
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(register.email)) {
            message = this.ERROR_MESSAGES.INVALID_EMAIL;
            return [false, message];
        }
        
        // Validate password match
        if (register.password !== register.repassword) {
            message = this.ERROR_MESSAGES.PASSWORDS_DO_NOT_MATCH;
            return [false, message];
        }

        // Validate logic conditions
        if(register.username.length < 3 || register.username.length > 20) {
            message = "Username must be at least 3 and at most 20 characters long";
            return [false, message];
        }
        if(register.password.length < 8 || register.password.length > 20) {
            message = "Password must be at least 8 and at most 20 characters long";
            return [false, message];
        }
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        if (!passwordRegex.test(register.password)) {
            message = "Password must contain at least one uppercase letter, "+
                        "one lowercase letter, one number, and one special character";
            return [false, message];
        }

        
        return [true];
    }

    public static validateChangePasswordRequest = (changePassword: ChangePasswordReqDTO): [boolean, string?] => {
        let message: string = "";
        if (!changePassword) {
            message = this.ERROR_MESSAGES.EMAIL_REQUIRED;
            return [false, message];
        }
        
        // Validate required fields
        if (!changePassword.email || changePassword.email.trim() === "") {
            message = this.ERROR_MESSAGES.EMAIL_REQUIRED;
            return [false, message];
        }
        if (!changePassword.newPassword || changePassword.newPassword.trim() === "") {
            message = this.ERROR_MESSAGES.PASSWORD_REQUIRED;
            return [false, message];
        }
        if (!changePassword.rePassword || changePassword.rePassword.trim() === "") {
            message = this.ERROR_MESSAGES.REPASSWORD_REQUIRED;
            return [false, message];
        }
        
        // Validate password match
        if (changePassword.newPassword !== changePassword.rePassword) {
            message = this.ERROR_MESSAGES.PASSWORDS_DO_NOT_MATCH;
            return [false, message];
        }

        // Validate logic conditions
        if(changePassword.newPassword.length < 8 || changePassword.newPassword.length > 20) {
            message = "New password must be at least 8 and at most 20 characters long";
            return [false, message];
        }
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        if (!passwordRegex.test(changePassword.newPassword)) {
            message = "New password must contain at least one uppercase letter, "+
                        "one lowercase letter, one number, and one special character";
            return [false, message];
        }
        
        return [true];
    }
};