import { NextRequest, NextResponse } from 'next/server';

const PUBLIC_ROUTES = ['/', '/unauthorized', '/forbidden', '/signin-oidc'];

function isPublicRoute(pathname: string): boolean {
    return PUBLIC_ROUTES.some(route =>
        pathname === route || pathname.startsWith(route + '/')
    );
}

export function proxy(req: NextRequest) {
    const { pathname } = req.nextUrl;
    const isAuthenticated = req.cookies.has('msal.session');

    console.log(`Proxy middleware: pathname=${pathname}, isAuthenticated=${isAuthenticated}`);

    if (!isAuthenticated && !isPublicRoute(pathname)) {
        return NextResponse.redirect(new URL('/', req.url));
    }

    if (isAuthenticated && pathname === '/') {
        return NextResponse.redirect(new URL('/dashboard', req.url));
    }

    return NextResponse.next();
}

export const config = {
    matcher: [
        '/((?!_next|[^?]*\\.(?:html?|css|js(?!on)|jpe?g|webp|png|gif|svg|ttf|woff2?|ico|csv|docx?|xlsx?|zip|webmanifest)).*)',
        '/(api|trpc)(.*)',
    ],
};
