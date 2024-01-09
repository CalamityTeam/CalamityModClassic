using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SlagMagnum : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Slag Magnum");
	}

    public override void SetDefaults()
    {
        Item.damage = 24;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 76;
        Item.height = 22;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 4f;
        Item.value = 100000;
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = true;
        Item.shootSpeed = 22f;
        Item.shoot = Mod.Find<ModProjectile>("SlagRound").Type;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("SlagRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}