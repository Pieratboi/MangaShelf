using MangaShelf.Application.Common.Exceptions;

namespace MangaShelf.Application.Chapters.Update;

public class UpdateChapterUseCase
{
    private readonly IChapterRepository _chapterRepository;

    public UpdateChapterUseCase(IChapterRepository chapterRepository)
    {
        _chapterRepository = chapterRepository;
    }

    public async Task<UpdateChapterResponse?> ExecuteAsync(
        int mangaId,
        int chapterId,
        UpdateChapterRequest request)
    {
        if(mangaId <= 0)
        {
            throw new ApplicationValidationException("Manga id must be greater than 0.");
        }

        if(chapterId <= 0)
        {
            throw new ApplicationValidationException("Chapter id must be greater than 0.");
        }

        if(request.Number is null && request.Title is null)
        {
            throw new ApplicationValidationException("At least one chapter detail must be provided.");
        }

        var chapter = await _chapterRepository.GetByIdForUpdateAsync(chapterId);

        if(chapter is null || chapter.MangaId != mangaId)
        {
            return null;
        }

        if(request.Number is not null)
        {
            var duplicateExist = await _chapterRepository
                .ExistsByMangaIdAndNumberExceptChapterAsync(
                    mangaId,
                    request.Number.Value,
                    chapterId);

            if (duplicateExist)
            {
                throw new ApplicationValidationException("A chapter with this number already exists for this manga.");
            }

            chapter.ChangeNumber(request.Number.Value);
        }

        if(request.Title is not null)
        {
            chapter.ChangeTitle(request.Title);
        }

        await _chapterRepository.SaveChangesAsync();

        return new UpdateChapterResponse
        {
            Id = chapter.Id,
            MangaId = chapter.MangaId,
            Number = chapter.Number,
            Title = chapter.Title
        };
    }
}