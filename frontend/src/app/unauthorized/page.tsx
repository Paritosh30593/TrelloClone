import Link from "next/link";
import { Ban, Home } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";

export default function UnauthorizedPage() {
    return (
        <main className="container mx-auto px-4 py-10 sm:py-16">
            <Card className="mx-auto max-w-xl">
                <CardHeader className="text-center">
                    <div className="mx-auto mb-4 flex h-12 w-12 items-center justify-center rounded-full bg-red-100">
                        <Ban className="h-6 w-6 text-red-600" />
                    </div>
                    <CardTitle className="text-2xl">Access denied</CardTitle>
                    <CardDescription>
                        You are signed in, but you do not have permission to view this page.
                    </CardDescription>
                </CardHeader>
                <CardContent className="flex justify-center gap-3">
                    <Button asChild>
                        <Link href="/dashboard">Go to Dashboard</Link>
                    </Button>
                    <Button variant="outline" asChild>
                        <Link href="/">
                            <Home className="mr-2 h-4 w-4" />
                            Back Home
                        </Link>
                    </Button>
                </CardContent>
            </Card>
        </main>
    );
}
