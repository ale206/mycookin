using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Media.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class MediaRepository : IMediaRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public MediaRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBMediaConnectionString");
        }

        public MediaByIdOut MediaById(MediaByIdIn mediaByIdIn)
        {
            MediaByIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<MediaByIdOut>("USP_GetMediaById",
                    new
                    {
                        IdMedia = mediaByIdIn.MediaId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public MediaUploadConfigParametersOut MediaUploadConfigParameters(MediaUploadConfigParametersIn mediaUploadConfigParametersIn)
        {
            MediaUploadConfigParametersOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<MediaUploadConfigParametersOut>("USP_GetMediaUploadConfigByMediaType",
                    new
                    {
                        MediaType = mediaUploadConfigParametersIn.MediaType.ToString()
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SaveOrUpdateMediaOut SaveOrUpdateMedia(SaveOrUpdateMediaIn saveOrUpdateMediaIn)
        {
            SaveOrUpdateMediaOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SaveOrUpdateMediaOut>("USP_SaveMedia",
                    new
                    {
                        IDMedia = saveOrUpdateMediaIn.MediaId,
                        IDMediaOwner = saveOrUpdateMediaIn.OwnerId,
                        isImage = saveOrUpdateMediaIn.IsImage,
                        isVideo = saveOrUpdateMediaIn.IsVideo,
                        isLink = saveOrUpdateMediaIn.IsLink,
                        isEsternalSource = saveOrUpdateMediaIn.IsAnExternalSource,
                        saveOrUpdateMediaIn.MediaServer,
                        saveOrUpdateMediaIn.MediaBakcupServer,
                        saveOrUpdateMediaIn.MediaPath,
                        MediaMD5Hash = saveOrUpdateMediaIn.MediaMd5Hash,
                        Checked = saveOrUpdateMediaIn.IsChecked,
                        saveOrUpdateMediaIn.CheckedByUser,
                        MediaDisabled = saveOrUpdateMediaIn.IsMediaDisabled,
                        MediaUpdatedOn = DateTime.UtcNow,
                        saveOrUpdateMediaIn.MediaDeletedOn,
                        UserIsMediaOwner = saveOrUpdateMediaIn.IsUserMediaOwner,
                        MediaOnCDN = saveOrUpdateMediaIn.IsMediaOnCdn,
                        saveOrUpdateMediaIn.MediaType
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<MediaAlternativeSizeConfigParametersOut> MediaAlternativeSizeConfigParameters(MediaAlternativeSizeConfigParametersIn mediaAlternativeSizeConfigParametersIn)
        {
            IEnumerable<MediaAlternativeSizeConfigParametersOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<MediaAlternativeSizeConfigParametersOut>>("USP_GetMediaAlternativeSizeConfigParametersByMediaType",
                    new
                    {
                        MediaType = mediaAlternativeSizeConfigParametersIn.MediaType.ToString()
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public AddAlternativePhotoSizeOut AddAlternativePhotoSize(AddAlternativePhotoSizeIn addAlternativePhotoSizeIn)
        {
            AddAlternativePhotoSizeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<AddAlternativePhotoSizeOut>("USP_SaveAlternativeSizeMedia",
                    new
                    {
                        IDMediaAlternativeSize = addAlternativePhotoSizeIn.MediaAlternativeSizeId,
                        IDMedia = addAlternativePhotoSizeIn.MediaId,
                        addAlternativePhotoSizeIn.MediaSizeType,
                        addAlternativePhotoSizeIn.MediaServer,
                        MediaBakcupServer = addAlternativePhotoSizeIn.MediaBackupServer,
                        addAlternativePhotoSizeIn.MediaPath,
                        MediaMD5Hash = addAlternativePhotoSizeIn.MediaMd5Hash,
                        MediaOnCDN = addAlternativePhotoSizeIn.MediaOnCdn
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DisableMediaOut DisableMedia(DisableMediaIn disableMediaIn)
        {
            DisableMediaOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DisableMediaOut>("USP_DisableMedia",
                    new
                    {
                        IdMedia = disableMediaIn.MediaId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<GetMediaNotInCdnOut> GetMediaNotInCdn(GetMediaNotInCdnIn getMediaNotInCdnIn)
        {
            IEnumerable<GetMediaNotInCdnOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<GetMediaNotInCdnOut>>("USP_SaveAlternativeSizeMedia",
                    new
                    {
                        getMediaNotInCdnIn.NumRow,
                        getMediaNotInCdnIn.MediaSizeType
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}