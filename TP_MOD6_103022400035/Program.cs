using System;

public class SayaMusicTrack
{
    private int id;
    private int playCount;
    private string title;

    // Constructor
    public SayaMusicTrack(string title)
    {
        this.title = title;
        this.playCount = 0;

        // Generate random 5 digit id
        Random rand = new Random();
        this.id = rand.Next(10000, 100000);
    }

    // Method untuk menambah play count
    public void IncreasePlayCount(int count)
    {
        this.playCount += count;
    }

    // Method untuk print detail track
    public void PrintTrackDetails()
    {
        Console.WriteLine("ID       : " + id);
        Console.WriteLine("Title    : " + title);
        Console.WriteLine("PlayCount: " + playCount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Membuat objek
        SayaMusicTrack track1 = new SayaMusicTrack("Hati-Hati di Jalan");

        // Menambah play count
        track1.IncreasePlayCount(5);
        track1.IncreasePlayCount(3);

        // Menampilkan detail
        track1.PrintTrackDetails();
    }
}