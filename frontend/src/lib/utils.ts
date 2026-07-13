import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

// Generic tailwind helper to merge class names
export const cn = (...inputs: ClassValue[]) => twMerge(clsx(inputs));


// Generic helper to get property name
type EnumLike = Record<string, string | number>;
type EnumMemberName<TEnum extends EnumLike> = Exclude<Extract<keyof TEnum, string>, `${number}`>;
export const nameof = <T>(
	name: T extends EnumLike ? EnumMemberName<T> : Extract<keyof T, string>
): string => name;


// Generic helper to get priority color
export const getPriorityColor = (priority: string) => {
	switch (priority) {
		case "Medium":
			return "bg-yellow-500";
		case "High":
			return "bg-red-500";
		case "Low":
		default:
			return "bg-green-500";
	}
};
