import { ReactNode } from "react";

interface SelectProps{
    children: ReactNode;
    title?: string;
}

function Select(props : SelectProps) {
    return (
        <select title={props.title}>
            {props.children}
        </select>
    )
}

export default Select;