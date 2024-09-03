import { useToken } from "hooks/useToken";
import { ReactNode } from "react";

interface NavigationBarProps {
    children: ReactNode
}
function NavigationBar({ children }: NavigationBarProps) {
    const { userObject, hasToken } = useToken();
    return (
        <>
            <nav className="bg-gray-800 text-white flex justify-between items-center p-4">
                <div className="flex items-center">
                    <span className="text-xl font-bold">Twoday Minds Todos</span>
                </div>
                {hasToken ? <ul className="hidden lg:flex space-x-8">
                    <li><a href="/" className="hover:text-gray-200">Home</a></li>
                    <li><a href="/todos" className="hover:text-gray-200">About</a></li>
                    <li><a href="#" className="hover:text-gray-200">Contact</a></li>
                </ul>
                    : <ul className="hidden lg:flex space-x-8">
                        <li><a href="/" className="hover:text-gray-200">Home</a></li>
                        <li><a href="/todos" className="hover:text-gray-200">About</a></li>
                        <li><a href="#" className="hover:text-gray-200">Contact</a></li>
                    </ul>}
                <button className="lg:hidden bg-gray-700 p-2 rounded-full hover:bg-gray-600">
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16m-7 6v1a1 1 0 01-1 1H3a1 1 0 01-1-1V3a1 1 0 011-1h2a1 1 0 011 1v3m-4 0h.01M4 18v1a1 1 0 001 1h16a1 1 0 001-1v-2a1 1 0 011-2H4a1 1 0 00-1-1V4a1 1 0 001-1h1M4 7a1 1 0 011-1m1 1a1 1 0 000 2v.01M4 12a1 1 0 010-2V4a1 1 0 010 2M4 17nNone.svg" />
                    </svg>
                </button>
            </nav>
            {children}
        </>

    );
}

export default NavigationBar;