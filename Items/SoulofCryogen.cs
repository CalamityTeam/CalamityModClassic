using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items
{
    [AutoloadEquip(EquipType.Wings)]
    public class SoulofCryogen : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            Terraria.ID.ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(60, 8, 1.35f);
        }

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Soul of Cryogen");
		////Tooltip.SetDefault("The magic of the ancient ice castle is yours\nCounts as wings\nDecent acceleration: 1.35\nDecent flight time: 60\n10% increase to all damage and pick speed\nFrost damage added to melee swings");
		Item.width = 26;
		Item.height = 26;
		Item.value = 500000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0f * num, 0.3f * num, 0.3f * num);
    }
	
    public override void VerticalWingSpeeds(Player p, ref float ascentWhenFalling, ref float ascentWhenRising,
       ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 0.85f;
        ascentWhenRising = 0.15f;
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 3f;
        constantAscend = 0.135f;
    }
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
		modPlayer.cryogenSoul = true;
		player.pickSpeed -= 0.1f;
		player.GetDamage(DamageClass.Magic) *= 1.1f;
		player.GetDamage(DamageClass.Ranged) *= 1.1f;
		player.GetDamage(DamageClass.Melee) *= 1.1f;
		player.GetDamage(DamageClass.Summon) *= 1.1f;
		player.GetDamage(DamageClass.Throwing) *= 1.1f;
		player.wingTimeMax = 60;
            if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
                Dust.NewDust(player.position, player.width, player.height, 67, 0, 0, 0, Color.Cyan);
        }
}}