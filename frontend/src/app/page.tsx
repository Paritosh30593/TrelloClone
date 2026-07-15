import { Navbar } from "@/components/common/navbar";
import { Fragment } from "react";

export default function Home() {
    return (
        <Fragment>
            <Navbar />
            <main className="container mx-auto px-4 py-6 sm:py-8"></main>
        </Fragment>
    );
}
