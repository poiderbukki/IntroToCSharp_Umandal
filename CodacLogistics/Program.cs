/*
 * =============================================================================
 * Codac Logistics - Driver Fuel & Delivery Performance Tracker
 * =============================================================================
 * Purpose: Console-based tool for delivery drivers to track daily fuel expenses
 *          and delivery performance over a 5-day work week.
 * Author:  Codac Logistics Development Team
 * Date:    February 2025
 * =============================================================================
 */

using System;

namespace CodacLogistics;

class Program
{
    static void Main(string[] args)
    {
        // ---------------------------------------------------------------------
        // TASK 1: Driver Profile & Distance Validation
        // ---------------------------------------------------------------------

        // string: Driver name is text; no arithmetic needed.
        Console.WriteLine("=== Codac Logistics - Fuel & Performance Tracker ===\n");
        Console.Write("Enter Driver's Full Name: ");
        string driverName = Console.ReadLine() ?? "";

        // decimal: Financial values (budget, expenses) use decimal for exact
        // representation and no floating-point rounding errors in money.
        Console.Write("Enter Weekly Fuel Budget (e.g. 2500.00): ");
        decimal weeklyFuelBudget = decimal.Parse(Console.ReadLine() ?? "0");

        // double: Distance in km can have decimals; scientific precision is
        // acceptable here, so double is suitable.
        double totalDistanceKm;
        const double MinDistance = 1.0;
        const double MaxDistance = 5000.0;

        // while loop: We must keep asking until valid input. "if" would only
        // check once; "while" repeats until the condition becomes false.
        while (true)
        {
            Console.Write($"Enter Total Distance Traveled this week (km, {MinDistance}-{MaxDistance}): ");
            totalDistanceKm = double.Parse(Console.ReadLine() ?? "0");

            if (totalDistanceKm >= MinDistance && totalDistanceKm <= MaxDistance)
                break;

            Console.WriteLine($"Error: Distance must be between {MinDistance} and {MaxDistance}. Please try again.\n");
        }

        // ---------------------------------------------------------------------
        // TASK 2: Fuel Expense Tracking (5-day array)
        // ---------------------------------------------------------------------

        // 1D array of decimal: Store exactly 5 daily fuel costs for the week.
        decimal[] fuelExpenses = new decimal[5];
        // decimal: Accumulator for money; keeps financial totals precise.
        decimal totalFuelSpent = 0;

        // for loop: Fixed number of iterations (5 days); index i (int) = 0..4.
        for (int i = 0; i < fuelExpenses.Length; i++)
        {
            // (i + 1): Array index is 0-based; display "Day 1".."Day 5" so we use (i + 1).
            Console.Write($"Enter fuel cost for Day {i + 1}: ");
            fuelExpenses[i] = decimal.Parse(Console.ReadLine() ?? "0");
            totalFuelSpent += fuelExpenses[i];
        }

        // ---------------------------------------------------------------------
        // TASK 3: Performance Analysis
        // ---------------------------------------------------------------------

        // Average = sum / count; decimal for money.
        decimal averageDailyFuelExpense = totalFuelSpent / 5;

        // Efficiency = km per unit fuel. Cast to double for division with totalDistanceKm.
        double totalFuelDouble = (double)totalFuelSpent;
        double kmPerUnitFuel = totalFuelDouble > 0 ? totalDistanceKm / totalFuelDouble : 0;

        // if/else: Three distinct efficiency bands; branching logic fits the
        // business rules (High/Standard/Low) without needing a loop.
        string efficiencyRating;
        if (kmPerUnitFuel > 15)
            efficiencyRating = "High Efficiency";
        else if (kmPerUnitFuel >= 10)
            efficiencyRating = "Standard Efficiency";
        else
            efficiencyRating = "Low Efficiency / Maintenance Required";

        // bool: Budget status is a true/false condition for the report.
        bool stayedUnderBudget = totalFuelSpent <= weeklyFuelBudget;

        // ---------------------------------------------------------------------
        // TASK 4: The Audit Report
        // ---------------------------------------------------------------------

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("                    AUDIT REPORT");
        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"Driver Name:              {driverName}");
        Console.WriteLine($"Total Distance (km):      {totalDistanceKm:N2}");
        Console.WriteLine($"Weekly Fuel Budget:       {weeklyFuelBudget:C2}");
        Console.WriteLine(new string('-', 60));
        Console.WriteLine("5-Day Fuel Expense Breakdown:");
        // for loop: Iterate over the 5-day array to display each day's expense.
        for (int i = 0; i < fuelExpenses.Length; i++)
            Console.WriteLine($"  Day {i + 1}: {fuelExpenses[i]:C2}");
        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"Total Fuel Spent:         {totalFuelSpent:C2}");
        Console.WriteLine($"Average Daily Expense:    {averageDailyFuelExpense:C2}");
        Console.WriteLine($"Fuel Efficiency Rating:   {efficiencyRating}");
        Console.WriteLine($"Stayed Under Budget:      {stayedUnderBudget}");
        Console.WriteLine(new string('=', 60));
        Console.WriteLine("\nReport generated for Codac Logistics Accounting.");
    }
}
