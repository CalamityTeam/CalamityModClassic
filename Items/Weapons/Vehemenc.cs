using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Vehemenc : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
	}

    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 590;
        Item.width = 44;
        Item.height = 44;
        Item.useTime = 50;
        Item.useAnimation = 50;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.75f;
        Item.value = 100000000;
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item73;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("Vehemence").Type;
        Item.shootSpeed = 16f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Vehemence").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	player.AddBuff(BuffID.ManaSickness, 600, true);
    	return false;
    }
}}