import { LoginRequest } from "hooks/useLogin";
import { BaseSyntheticEvent, ReactNode } from "react";

interface FormProps {
    children: ReactNode;
    classNames?: string;
    handleSubmit?: (e?: BaseSyntheticEvent<object, any, any>) => Promise<void>

}
function Form({ children, classNames, handleSubmit }: FormProps) {
    return (<form onSubmit={handleSubmit} className={classNames}>
        {children}
    </form>)
}

export default Form;