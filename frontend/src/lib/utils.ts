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

// Generic helper to convert a date string to a date key (YYYY-MM-DD)
export const toDateKey = (value: string | null | undefined): string | null => {
	if (!value) return null;
	if (/^\d{4}-\d{2}-\d{2}$/.test(value)) return value;

	const date = new Date(value);
	if (Number.isNaN(date.getTime())) return null;

	const year = date.getFullYear();
	const month = String(date.getMonth() + 1).padStart(2, "0");
	const day = String(date.getDate()).padStart(2, "0");

	return `${year}-${month}-${day}`;
};