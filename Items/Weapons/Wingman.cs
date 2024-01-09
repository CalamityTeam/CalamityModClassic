using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Wingman : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Wingman");
		}

    public override void SetDefaults()
    {
        Item.damage = 54;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 12;
        Item.width = 32;
        Item.height = 22;
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3f;
        Item.value = 500000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item33;
        Item.autoReuse = true;
        Item.shootSpeed = 25f;
        Item.shoot = ProjectileID.LaserMachinegunLaser;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int num6 = 3;
        for (int index = 0; index < num6; ++index)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, 440, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
	}
}}