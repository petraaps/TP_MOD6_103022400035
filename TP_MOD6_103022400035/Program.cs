using System;
using System.Diagnostics;

public class SayaMusicTrack
{
    // Field private sesuai prinsip encapsulation
    private int id;
    private int playCount;
    private string title;

    // Constructor
    public SayaMusicTrack(string title)
    {
        // Precondition
        // Menggunakan Debug.Assert
        Debug.Assert(title != null, "Judul track tidak boleh null.");
        Debug.Assert(title == null || title.Length <= 100, "Judul track maksimal 100 karakter.");
        //Validasi
        if (title == null)
            throw new ArgumentNullException(nameof(title), "Judul track tidak boleh null.");

        if (title.Length > 100)
            throw new ArgumentException("Judul track maksimal 100 karakter.");

        // Inisialisasi nilai
        this.title = title;
        this.playCount = 0;

        // Generate ID random 5 digit
        Random rand = new Random();
        this.id = rand.Next(10000, 100000);
    }

    // Method untuk menambah play count
    public void IncreasePlayCount(int count)
    {
        // Precondition
        Debug.Assert(count <= 10000000, "Penambahan maksimal 10.000.000.");
        Debug.Assert(count >= 0, "Penambahan tidak boleh negatif.");

        // Validasi
        if (count < 0)
            throw new ArgumentException("Penambahan play count tidak boleh negatif.");

        if (count > 10000000)
            throw new ArgumentException("Penambahan play count maksimal 10.000.000.");

        try
        {
            // Overflow jika playcount sudah mau mencapai batas maksimal
            // checked digunakan untuk mendeteksi overflow integer
            checked
            {
                playCount += count;
            }
        }
        catch (OverflowException ex)
        {
            // Menangkap error overflow agar program tidak berhenti
            Console.WriteLine("Terjadi overflow saat menambah play count: " + ex.Message);

            // Lempar kembali agar bisa ditangani di Main jika perlu
            throw;
        }
    }

    // Method untuk menampilkan detail track
    public void PrintTrackDetails()
    {
        Console.WriteLine("ID        : " + id);
        Console.WriteLine("Title     : " + title);
        Console.WriteLine("Play Count: " + playCount);
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Test
        try
        {
            SayaMusicTrack track1 = new SayaMusicTrack("Hati-Hati di Jalan");

            // Menambah play count normal
            track1.IncreasePlayCount(5000);

            // Menampilkan data
            track1.PrintTrackDetails();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        // Test Precondition Title > 100
        Console.WriteLine("Test Precondition Title > 100");
        try
        {
            // Membuat string lebih dari 100 karakter
            string longTitle = new string('A', 101);

            // Harus gagal karena melanggar precondition
            SayaMusicTrack track2 = new SayaMusicTrack(longTitle);
            track2.PrintTrackDetails();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        //Test Precondition count > 10000000
        Console.WriteLine("Test Precondition count > 10000000");
        try
        {
            SayaMusicTrack track3 = new SayaMusicTrack("Track Uji Count");

            // Seharusnya gagal karena melebihi batas
            track3.IncreasePlayCount(10000001);

            track3.PrintTrackDetails();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        // Test Overflow
        Console.WriteLine("Test Overflow");
        try
        {
            SayaMusicTrack track4 = new SayaMusicTrack("Track Overflow");

            // Loop untuk mempercepat terjadinya overflow
            for (int i = 0; i < 300; i++)
            {
                track4.IncreasePlayCount(10000000);
            }

            track4.PrintTrackDetails();
        }
        catch (OverflowException ex)
        {
            // Menangkap overflow agar program tetap berjalan
            Console.WriteLine("Terjadi overflow saat menambah play count: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}