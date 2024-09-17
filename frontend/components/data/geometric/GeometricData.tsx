import { Scalars } from "../../../__generated__/__types__";
import { useGeometricDataQuery } from "../../../queries/data.graphql";
import { Skeleton, Result, Descriptions, Typography } from "antd";
import { PageHeader } from "@ant-design/pro-layout";
import { useEffect } from "react";
import Link from "next/link";
import paths from "../../../paths";
import { messageApolloError } from "../../../lib/apollo";

export type GeometricDataProps = {
    geometricDataId: Scalars["Uuid"];
};

export default function GeometricData({ geometricDataId }: GeometricDataProps) {
    const { loading, error, data } = useGeometricDataQuery({
        variables: {
            uuid: geometricDataId,
        },
    });

    const geometricData = data?.geometricData;


    useEffect(() => {
        if (error) {
            messageApolloError(error);
        }
    }, [error]);

    if (loading) {
        return <Skeleton active avatar title />;
    }

    if (!geometricData) {
        return (
            <Result
                status="500"
                title="500"
                subTitle="Sorry, something went wrong."
            />
        );
    }

    return (
        <PageHeader
            title={geometricData.name}
            subTitle={geometricData.description}

            backIcon={false}
        >
            <Descriptions size="small" column={1}>
                <Descriptions.Item label="UUID">{geometricData.uuid}</Descriptions.Item>
                <Descriptions.Item label="Component">
                    <Typography.Link
                        href={paths.metabase.component(geometricData.componentId)}
                    >
                        {geometricData.componentId}
                    </Typography.Link>
                </Descriptions.Item>
                <Descriptions.Item label="Creator">
                    <Link
                        href={paths.metabase.institution(geometricData.creatorId)}
                        legacyBehavior>
                        {geometricData.creatorId}
                    </Link>
                </Descriptions.Item>
            </Descriptions>
        </PageHeader>
    );
}