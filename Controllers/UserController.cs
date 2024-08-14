using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWebsiteAPI.Contexts;
using PersonalWebsiteAPI.Entities;

namespace PersonalWebsiteAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationContext _context;

    public UserController(ApplicationContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Ambil data dari database.
        var listUsers = await _context.User.ToListAsync();

        // Validasi data ada atau tidak.
        if (listUsers.Count > 0)
        {
            // response jika data ditemukan.
            return StatusCode(StatusCodes.Status200OK, new
            {
                Message = "Data ditemukan",
                Data = listUsers
            });
        }
        else
        {
            // response jika data tidak ditemukan.
            return StatusCode(StatusCodes.Status404NotFound, new
            {
                Message = "Data tidak ditemukan",
                Data = listUsers
            });
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByID(int id)
    {
        // ambil data dari database berdasarkan primary key table.
        var userResult = await _context.User.FindAsync(id);
        if (userResult != null)
        {
            // response jika data ditemukan.
            return StatusCode(StatusCodes.Status200OK, new
            {
                Message = "Data ditemukan",
                Data = userResult
            }); 
        }
        else
        {
            // response jika data tidak ditemukan.
            return StatusCode(StatusCodes.Status404NotFound, new
            {
                Message = "Data tidak ditemukan",
                Data = userResult
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserEntity userRequest)
    {
        // Set id 0 karna key pada tabel auto generate.
        userRequest.id = 0;

        // validasi untuk menambahkan data baru.
        if (string.IsNullOrEmpty(userRequest.name))
        {
            // jika attribute nama kosong maka buat response bad request.
            return StatusCode(StatusCodes.Status400BadRequest, new
            {
                Message = "Nama tidak boleh kosong."
            });
        }
        if (string.IsNullOrEmpty(userRequest.email))
        {
            // jika attribute email kosong maka buat response bad request.
            return StatusCode(StatusCodes.Status400BadRequest, new
            {
                Message = "Email tidak boleh kosong."
            });
        }
        if (Regex.IsMatch(userRequest.email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
        {
            // jika attribute email jika bukan format email maka buat response bad request.
            return StatusCode(StatusCodes.Status400BadRequest, new
            {
                Message = "Format email tidak valid."
            });
        }
        if (string.IsNullOrEmpty(userRequest.job_title))
        {
            // jika attribute job title kosong maka buat response bad request.
            return StatusCode(StatusCodes.Status400BadRequest, new
            {
                Message = "Job title tidak boleh kosong."
            });
        }

        // simpan data ke database.
        _context.User.Add(userRequest);
        var saveResult = await _context.SaveChangesAsync();

        // cek hasil simpan
        if (saveResult > 0)
        {
            return StatusCode(StatusCodes.Status201Created, new
            {
                Message = "Simpan data berhasil",
                Data = userRequest
            });
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = "Gagal simpan data."
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(UserEntity userRequest)
    {
        // cari data berdasarkan key
        var userSearch = await _context.User.Where(s => s.id == userRequest.id).SingleOrDefaultAsync();
        if (userSearch == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, new
            {
                Message = "User tidak ditemukan."
            });
        }
        else
        {
            userSearch.id = userRequest.id;
            userSearch.name = userRequest.name;
            userSearch.email = userRequest.email;
            userSearch.image = userRequest.image;
            userSearch.job_title = userRequest.job_title;
            userSearch.whatsapp = userRequest.whatsapp;
            userSearch.github_url = userRequest.github_url;
            userSearch.linkedin_url = userRequest.linkedin_url;
            userSearch.bio = userRequest.bio;
            _context.User.Update(userSearch);
            if (await _context.SaveChangesAsync() > 0)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    Message = "Ubah data berhasil",
                    Data = userSearch
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Gagal ubah data."
                });
            }
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // cari data berdasarkan key
        var userSearch = await _context.User.Where(s => s.id == id).SingleOrDefaultAsync();
        if (userSearch == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, new
            {
                Message = "User tidak ditemukan."
            });
        }
        else
        {
            _context.User.Remove(userSearch);
            if (await _context.SaveChangesAsync() > 0)
            {
                return StatusCode(StatusCodes.Status201Created, new
                {
                    Message = "Hapus data berhasil"
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Gagal hapus data."
                });
            }
        }
    }
}
