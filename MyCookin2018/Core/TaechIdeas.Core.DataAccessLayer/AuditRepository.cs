using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.Audit;
using TaechIdeas.Core.Core.Audit.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class AuditRepository : IAuditRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AuditRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBAuditConnectionString");
        }

        public AutoAuditConfigInfoOut AutoAuditConfigInfo(AutoAuditConfigInfoIn autoAuditConfigInfoIn)
        {
            AutoAuditConfigInfoOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<AutoAuditConfigInfoOut>("USP_GetAutoAuditConfigInfoByObjectType",
                    new
                    {
                        ObjectType = autoAuditConfigInfoIn.ObjectType.ToString()
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NewAuditEventOut NewAuditEvent(NewAuditEventIn newAuditEventIn)
        {
            NewAuditEventOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewAuditEventOut>("USP_AddAuditEvent",
                    new
                    {
                        newAuditEventIn.AuditEventMessage,
                        ObjectID = newAuditEventIn.ObjectId,
                        ObjectType = newAuditEventIn.ObjectType.ToString(),
                        newAuditEventIn.ObjectTxtInfo,
                        newAuditEventIn.AuditEventLevel,
                        EventInsertedOn = DateTime.UtcNow,
                        AuditEventIsOpen = true
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateAuditEventOut UpdateAuditEvent(UpdateAuditEventIn updateAuditEventIn)
        {
            UpdateAuditEventOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateAuditEventOut>("USP_UpdateAuditEvent",
                    new
                    {
                        IDAuditEvent = updateAuditEventIn.AuditEventId,
                        IDEventUpdatedBy = updateAuditEventIn.EventUpdatedById,
                        AuditEventIsOpen = updateAuditEventIn.IsEventAuditOpen,
                        EventUpdatedOn = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckUserSpamReportedOut CheckUserSpamReported(CheckUserSpamReportedIn checkUserSpamReportedIn)
        {
            CheckUserSpamReportedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckUserSpamReportedOut>("USP_CheckUserSpamReported",
                    new
                    {
                        IDUser1 = checkUserSpamReportedIn.UserId1,
                        IDUser2 = checkUserSpamReportedIn.UserId2
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteByObjectIdOut DeleteByObjectId(DeleteByObjectIdIn deleteByObjectIdIn)
        {
            DeleteByObjectIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteByObjectIdOut>("USP_DeleteByObjectId",
                    new
                    {
                        ObjectID = deleteByObjectIdIn.ObjectId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GetAuditEventByIdOut GetAuditEventById(GetAuditEventByIdIn getAuditEventByIdIn)
        {
            GetAuditEventByIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetAuditEventByIdOut>("USP_GetAuditEventById",
                    new
                    {
                        IDAuditEvent = getAuditEventByIdIn.AuditEventId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<GetAuditEventToCheckOut> GetAuditEventToCheck(GetAuditEventToCheckIn getAuditEventToCheckIn)
        {
            List<GetAuditEventToCheckOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<List<GetAuditEventToCheckOut>>("USP_GetAuditEventByObjectType",
                    new
                    {
                        getAuditEventToCheckIn.NumberOfResults,
                        getAuditEventToCheckIn.ObjectType
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GetNumberOfEventToCheckOut GetNumberOfEventToCheck(GetNumberOfEventToCheckIn getNumberOfEventToCheckIn)
        {
            GetNumberOfEventToCheckOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GetNumberOfEventToCheckOut>("USP_GetNumberOfEventToCheck",
                    new
                    {
                        getNumberOfEventToCheckIn.ObjectType
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<GetObjectIdNumberOfEveniencesOut> GetObjectIdNumberOfEveniences(GetObjectIdNumberOfEveniencesIn getObjectIdNumberOfEveniencesIn)
        {
            List<GetObjectIdNumberOfEveniencesOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<List<GetObjectIdNumberOfEveniencesOut>>("USP_GetObjectIdNumberOfEveniences",
                    new
                    {
                        getObjectIdNumberOfEveniencesIn.ObjectType
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}