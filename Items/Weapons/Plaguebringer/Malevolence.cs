using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Plaguebringer {
public class Malevolence : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Malevolence");
	}

    public override void SetDefaults()
    {
        Item.damage = 51;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 36;
        Item.height = 58;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3f;
        Item.value = 500000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item97;
        Item.autoReuse = true;
        Item.shootSpeed = 12f;
        Item.shoot = Mod.Find<ModProjectile>("PlagueArrow").Type; //idk why but all the guns in the vanilla source have this
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	for (int i = 0; i < 2; i++) // Will shoot 2 arrows
    	{
            float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("PlagueArrow").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}
}}